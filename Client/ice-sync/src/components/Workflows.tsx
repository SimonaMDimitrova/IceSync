import { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";

import Workflow from "../models/workflow.model";
import workflowsService from "../services/workflows-service";

import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.css';

export default function Workflows() {
    const [workflows, setWorkflows] = useState<Workflow[]>([]);

    useEffect(() => {
        workflowsService.getAll()
            .then(workflows =>
            {
                setWorkflows(workflows)
            });
    }, [])

    function runWorkflow(id: number) {
        workflowsService.run(id)
            .then(() => {
                toast.success("Successfully run workflow!", {
                    position: "top-right"
                });
            })
            .catch(err => {
                toast.error("Unsuccessfully run workflow!", {
                    position: "top-right"
                });
            })
    }

    return (
        <div className="col-6 container justify-content-center">
            <h2 className="h2 text-center">Workflows</h2>
            <hr />
            <table className="table">
                <thead>
                    <tr>
                        <th>Workflow Id</th>
                        <th>Workflow Name</th>
                        <th>Is Active</th>
                        <th>Multi Exec Behavior</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {workflows?.map((workflow: Workflow) => (
                        <tr key={workflow.Id}>
                            <td>{workflow.Id}</td>
                            <td>{workflow.Name}</td>
                            <td>{workflow.IsActive.toString()}</td>
                            <td>{workflow.MultiExecBehavior}</td>
                            <td onClick={() => runWorkflow(workflow.Id)}>
                                {
                                    workflow.IsActive ? <button type="button" className="btn btn-dark">Run</button> : ""
                                }
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <ToastContainer />
        </div>
    );
}