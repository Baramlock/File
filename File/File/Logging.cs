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
            Pathfinder writeInConsoleInFridayToFile = new Pathfinder(new ConsoleLogWritter(new PolicySendIfFriday()), new FileLogWritter(new PolicySendfAlways()));
        }
    }

    interface ILogger
    {
        void WriteMessage(string message);
    }

    interface IWritePolicy
    {
        bool IsWrite();
    }

    internal class PolicySendfAlways : IWritePolicy
    {
        public bool IsWrite() => true;
    }

    internal class PolicySendIfFriday : IWritePolicy
    {
        public bool IsWrite() => DateTime.Now.DayOfWeek == DayOfWeek.Friday;
    }

    internal class Pathfinder : ILogger
    {
        private readonly IEnumerable<ILogger> _loggers;

        public Pathfinder(params ILogger[] loggers)
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

        public void Find(string message, ILogger logger)
        {
            logger.WriteMessage(message);
        }
    }

    internal class ConsoleLogWritter : LogWritter
    {
        public ConsoleLogWritter(IWritePolicy writePolicy) : base(writePolicy) { }

        protected override void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    internal class FileLogWritter : LogWritter
    {
        public FileLogWritter(IWritePolicy writePolicy) : base(writePolicy) { }

        protected override void Write(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    internal abstract class LogWritter : ILogger
    {
        protected readonly IWritePolicy _writePolicy;

        public LogWritter(IWritePolicy writePolicy)
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
}