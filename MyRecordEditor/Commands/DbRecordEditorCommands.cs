using System.Windows.Input;

namespace DbRecordEditor
{
    public static class DbRecordEditorCommands
    {
        private static RoutedUICommand _save;

        static DbRecordEditorCommands()
        {
            _save = new RoutedUICommand("Save to Database", "CommandSave", typeof(DbRecordEditorCommands));
        }

        public static RoutedUICommand CommandSave
        {
            get { return _save; }
        }

    }
}
