namespace DocumentHierarchy
{
    public class FlagsEventArgs
    {
        public string Message { get; set; } // TODO: [Design] Пользователю лучше вернуть имя файла или папки. Может он как раз по нему принимает решение остановить поиск или нет.

        public bool FlagToStopSearch { get; set; }

        public FlagsEventArgs(string message)
        {
            Message = message;
        }
    }
}
