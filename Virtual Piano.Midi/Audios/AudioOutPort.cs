using System;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Storage;

namespace Virtual_Piano.Midi
{
    public sealed class AudioOutPort : IDisposable
    {
        private AudioGraph Graph;
        private AudioDeviceOutputNode DeviceOutputNode;
        private AudioSubmixNode SubmixNode;

        private AudioOutPort()
        {
        }

        public async static Task<AudioOutPort> CreateAsync()
        {
            CreateAudioGraphResult result = await AudioGraph.CreateAsync(new AudioGraphSettings(AudioRenderCategory.Media));
            if (result.Status != AudioGraphCreationStatus.Success) return null;

            CreateAudioDeviceOutputNodeResult deviceOutputNodeResult = await result.Graph.CreateDeviceOutputNodeAsync();
            if (deviceOutputNodeResult.Status != AudioDeviceNodeCreationStatus.Success) return null;

            AudioSubmixNode submixNode = result.Graph.CreateSubmixNode();
            submixNode.AddOutgoingConnection(deviceOutputNodeResult.DeviceOutputNode); // Add

            result.Graph.Start();

            return new AudioOutPort
            {
                Graph = result.Graph,
                DeviceOutputNode = deviceOutputNodeResult.DeviceOutputNode,
                SubmixNode = submixNode
            };
        }

        public bool Remove(AudioFileInputNode node)
        {
            if (node is null) return false;

            node.RemoveOutgoingConnection(this.SubmixNode); // Remove
            node.Dispose();

            return true;
        }

        public async Task<AudioFileInputNode> AddAsync(StorageFile file)
        {
            if (file is null) return null;

            CreateAudioFileInputNodeResult fileInputNodeResult = await this.Graph.CreateFileInputNodeAsync(file);
            if (fileInputNodeResult.Status != AudioFileNodeCreationStatus.Success) return null;

            AudioFileInputNode fileInputNode = fileInputNodeResult.FileInputNode;

            fileInputNode.Stop();
            fileInputNode.AddOutgoingConnection(this.SubmixNode, 1); // Add
            return fileInputNode;
        }

        public void Dispose()
        {
            if (this.SubmixNode != null)
            {
                this.SubmixNode.RemoveOutgoingConnection(this.DeviceOutputNode); // Remove
                this.SubmixNode.Dispose();
            }

            this.DeviceOutputNode?.Dispose();
            this.Graph?.Dispose();
        }
    }
}