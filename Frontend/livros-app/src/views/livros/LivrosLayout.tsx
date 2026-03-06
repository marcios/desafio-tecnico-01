import { Outlet } from "react-router-dom";

export default function AutoresLayout() {
    return <>
        <div className="card">
            <div className="card-body">
                <Outlet />
            </div>
        </div>
    </>
}