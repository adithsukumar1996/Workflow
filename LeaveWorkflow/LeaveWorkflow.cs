using System;
using System.Collections.Generic;

namespace Workflow {

    public class LeaveWorkflow {

        private const string MANAGER = "manager";
        private const string APPLICANT = "applicant";

        public Workflow<LeaveState> CreateLeaveWorkflow () => new Workflow<LeaveState> () {

            name = "Leave Workflow",
                getState = LeaveStateMock.GetState,
                updateState = LeaveStateMock.SetState,
                mutations = new List<Mutation<LeaveState>> () {
                    new Mutation<LeaveState> () {
                            name = "Save Leave Request",
                                roles = new List<string> () { APPLICANT },
                                predicate = (state) => state == null,
                                mutationHandler = (prevState, newState) => {
                                    newState.Status = LeaveStateStatus.Saved;
                                    return newState;
                                }
                        },
                        new Mutation<LeaveState> () {
                            name = "Submit Leave Request",
                                roles = new List<string> () { APPLICANT },
                                predicate = (state) => state == null || state.Status == LeaveStateStatus.Saved,
                                mutationHandler = (prevState, newState) => {
                                    newState.Status = LeaveStateStatus.Submitted;
                                    return newState;
                                }
                        },
                        new Mutation<LeaveState> () {
                            name = "Approve Leave Request",
                                roles = new List<string> () { MANAGER },
                                predicate = (state) => state.Status == LeaveStateStatus.Submitted,
                                mutationHandler = (prevState, newState) => {
                                    prevState.Status = LeaveStateStatus.Approved;
                                    prevState.Comment = newState.Comment;
                                    return prevState;
                                }
                        }
                }

        };
    }
}