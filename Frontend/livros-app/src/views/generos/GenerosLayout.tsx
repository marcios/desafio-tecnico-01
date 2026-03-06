import { Outlet } from "react-router-dom";

export default function GeneroLayuot() {
    return <>
        <div className="card">
            <div className="card-body">
                <Outlet />
            </div>
        </div>
    </>
}