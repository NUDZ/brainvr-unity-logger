namespace BrainVR.UnityLogger.Interfaces
{
    public enum ExperimentState { }
    public enum ExperimentEvent { }
    public enum TrialState { }
    public enum TrialEvent { }

    public delegate void ExperimentStateHandler(IExperiment ex, ExperimentState fromState, ExperimentState toState);
    public delegate void ExperimentEventHandler(IExperiment ex, ExperimentEvent experimentEvent);
    public delegate void TrialStateHandler(IExperiment ex, TrialState fromState, TrialState toState);
    public delegate void TrialEventHandler(IExperiment ex, string s);
    public delegate void ExperimentCustomMessageHandler(IExperiment ex, string message);

    public interface IExperiment
    {
        string Name { get; }
        int TrialNumber { get; }
        int ExperimentNumber { get; }

        event ExperimentStateHandler ExperimentStateChanged;
        event ExperimentEventHandler ExpeirmentEventSent;
        event TrialStateHandler TrialStateChanged;
        event TrialEventHandler TrialEventSent;
        event ExperimentCustomMessageHandler SendExperimentMessage;

        string ExperimentHeaderLog();
    }
}