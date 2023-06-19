using System;
using System.Collections.Generic;
using System.Linq;

namespace Workflow {
    public class Mutation<T> where T : class {
        public string name { get; set; }

        public Predicate<T> predicate { get; set; }

        public Func<T, T, T> mutationHandler { get; set; }

        public IEnumerable<string> roles { get; set; }

    }

    public class Workflow<T> where T : class {
        public string name { get; set; }

        public IEnumerable<Mutation<T>> mutations { get; set; }

        public Func<T> getState { get; set; }

        public Action<T> updateState { get; set; }

        public (T, IEnumerable<Mutation<T>>) GetMutations (IEnumerable<string> availableRoles) =>
            (this.getState (), this.mutations.Where (m => m.predicate (this.getState ()) && m.roles.Any (r => availableRoles.Contains (r))));

        public void InvokeMutation (Mutation<T> mutation, T newState) => this.updateState (mutation.mutationHandler (this.getState (), newState));
    }
}