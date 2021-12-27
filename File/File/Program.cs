namespace Lesson
{
    internal class Logging
    {
        static void Main(string[] args)
        {
            Pathfinder writeInFile = new Pathfinder(new FileLogWritter(new PolicySendfAlways()));
            Pathfinder writeInConsole = new Pathfinder(new ConsoleLogWritter(new PolicySendfAlways()));
            Pathfinder fridayWriteInFile = new Pathfinder(new FileLogWritter(new PolicySendIfFriday()));
            Pathfinder fridayWriteInConsole = new Pathfinder(new ConsoleLogWritter(new PolicySendIfFriday()));
            Pathfinder writeInConsoleInFridayToFile = new Pathfinder(new LogsWriter(
                new ConsoleLogWritter(new PolicySendIfFriday()),
                new FileLogWritter(new PolicySendfAlways())));
        }
    }

    interface ILogger
    {
        void WriteMessage(string message);
    }

    interface ICanWrite
    {
        bool IsWrite();
    }

    internal class PolicySendfAlways : ICanWrite
    {
        public bool IsWrite() => true;
    }

    internal class PolicySendIfFriday : ICanWrite
    {
        public bool IsWrite() => DateTime.Now.DayOfWeek == DayOfWeek.Friday;
    }

    internal class Pathfinder : ILogger
    {
        private readonly ILogger _loggers;

        public Pathfinder(ILogger loggers)
        {
            _loggers = loggers;
        }

        public void WriteMessage(string message)
        {

            _loggers.WriteMessage(message);
        }

        public void Find(string message, ILogger logger)
        {
            logger.WriteMessage(message);
        }
    }

    internal class ConsoleLogWritter : LogWritter
    {
        public ConsoleLogWritter(ICanWrite writePolicy) : base(writePolicy) { }

        protected override void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class FileLogWritter : LogWritter
    {
        public FileLogWritter(ICanWrite writePolicy) : base(writePolicy) { }

        protected override void Write(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    internal abstract class LogWritter : ILogger
    {
        protected readonly ICanWrite _writePolicy;

        public LogWritter(ICanWrite writePolicy)
        {
            _writePolicy = writePolicy;
        }

        public void WriteMessage(string message)
        {
            if (_writePolicy.IsWrite())
                Write(message);

        }

        protected abstract void Write(string message);
    }

    internal class LogsWriter : ILogger
    {
        private readonly ILogger[] _loggers;

        public LogsWriter(params ILogger[] loggers)
        {
            _loggers = loggers;
        }

        public void WriteMessage(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.WriteMessage(message);
            }
        }
    }
}