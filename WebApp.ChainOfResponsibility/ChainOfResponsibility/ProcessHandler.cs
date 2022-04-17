namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler _NextProcessHandler;

        public virtual object Handle(object processHandler)
        {
            if(_NextProcessHandler != null)
            {
                return _NextProcessHandler.Handle(processHandler);
            }

            return null;
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            _NextProcessHandler = processHandler;
            return _NextProcessHandler;
        }
    }
}
