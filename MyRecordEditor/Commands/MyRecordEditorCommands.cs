using System.Windows.Input;

namespace MyRecordEditor
{
    public static class MyRecordEditorCommands
    {
        private static RoutedUICommand _save;

        static MyRecordEditorCommands()
        {
            _save = new RoutedUICommand("Save to Database", "CommandSave", typeof(MyRecordEditorCommands));
        }

        public static RoutedUICommand CommandSave
        {
            get { return _save; }
        }

    }
}
