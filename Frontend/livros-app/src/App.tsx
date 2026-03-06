import { useEffect, useState } from 'react'
import GeneroService from './services/GeneroService.ts'
import { BrowserRouter, Link, Outlet, Route, Routes } from 'react-router-dom'
import Home from './views/HomeView.tsx'
import LivrosView from './views/LivrosView.tsx'
import GenerosView from './views/generos/GenerosView.tsx'
import GeneroCadastroView from './views/generos/CadastroView.tsx'
import LayoutView from './views/LayoutView.tsx'
import GeneroLayuot from './views/generos/GenerosLayout.tsx'
import AutoresLayout from './views/autores/AutoresLayout.tsx'
import AutoresViews from './views/autores/AutoresView.tsx'
import AutorCadastroView from './views/autores/CadastroView.tsx'

function App() {
  const [count, setCount] = useState(0)

  async function obterGeneros() {
    let generos = await GeneroService.obtertTodos();
    console.log('aaa==>', generos);
  }

  useEffect(() => {
    obterGeneros();

  }, []);

  return (


    <BrowserRouter>


      <Routes>
        {/* Define routes with a path and an element (component) */}
        <Route path="/" element={<LayoutView />} >
          <Route path="generos" element={<GeneroLayuot />} >
            <Route index element={<GenerosView />} />
            <Route path='cadastro/:generoId' element={<GeneroCadastroView />} />
          </Route>
          <Route path='autores' element={<AutoresLayout />} >
            <Route index element={<AutoresViews />} />
            <Route path='cadastro/:autorId' element={<AutorCadastroView />} />
          </Route>


          {/* <Route path="*" element={<LayoutView />} /> */}
        </Route>
      </Routes>
      <Outlet />
    </BrowserRouter>




  )
}

export default App
