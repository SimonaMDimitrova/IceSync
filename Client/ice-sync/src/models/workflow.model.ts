class Workflow {
    constructor(
        public Id: number,
        public Name: string,
        public IsActive: boolean,
        public MultiExecBehavior: string
    ) {}
}

export default Workflow;