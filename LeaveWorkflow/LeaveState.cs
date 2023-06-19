namespace Workflow {
    public class LeaveState {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string LeaveType { get; set; }

        public LeaveStateStatus Status { get; set; }

        public string Comment { get; set; }
    }

    public enum LeaveStateStatus {
        Saved,
        Submitted,
        Approved,
        Rejected
    }

    public class LeaveStateMock {
        private static LeaveState state = new LeaveState ();

        public static LeaveState GetState () => state;

        public static void SetState (LeaveState newState) => state = newState;
    }
}