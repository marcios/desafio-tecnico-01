import { useEffect, useState } from 'react'
import './App.css'

import GeneroService from './services/GeneroService.ts'
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom'
import Home from './views/HomeView.tsx'
import LivrosView from './views/LivrosView.tsx'
import GenerosView from './views/GenerosView.tsx'

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
      <nav>
        
        <Link to="/">Livros</Link>&nbsp; | &nbsp;
        <Link to="/generos">Gêneros</Link>
      </nav>

      <Routes>
        {/* Define routes with a path and an element (component) */}
        <Route path="/" element={<LivrosView />} />
        <Route path="/generos" element={<GenerosView />} />

        {/* Fallback route for any path that doesn't match */}
        <Route path="*" element={<Home />} />
      </Routes>
    </BrowserRouter>




  )
}

export default App
