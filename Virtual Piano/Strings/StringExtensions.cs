using Virtual_Piano.Midi;

namespace Virtual_Piano.Strings
{
    public static class StringExtensions
    {
        public static string GetString(this UIType type)
        {
            return App.Resource.GetString($"{type}");
        }

        public static string GetString(this MidiProgramGroup group)
        {
            return App.Resource.GetString($"{group}");
        }

        public static string GetString(this MidiProgram program)
        {
            return App.Resource.GetString($"{program}");
        }

        public static string GetString(this MidiPercussionProgram program)
        {
            return App.Resource.GetString($"{program}");
        }

        public static string GetString(this MidiPercussionNote note)
        {
            return App.Resource.GetString($"{note}");
        }
    }
}