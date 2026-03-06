import { Link, Outlet } from "react-router-dom";

export default function LayoutView() {
    return <>

        <ul className="nav justify-content-center">
            <li className="nav-item">
                <Link to="/livros" className="nav-link">Livros</Link>
            </li>
            <li className="nav-item">
                <Link to="/generos" className="nav-link">Gêneros</Link>
            </li>
              <li className="nav-item">
                <Link to="/autores" className="nav-link">Autores</Link>
            </li>
          
        </ul>
        <nav>

            
            
        </nav>
        <hr />
        <div className="container-fluid">
            <Outlet />
        </div>
    </>
}