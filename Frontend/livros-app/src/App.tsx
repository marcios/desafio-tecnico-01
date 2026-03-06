import { useEffect } from 'react'
import GeneroService from './services/GeneroService.ts'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import GenerosView from './views/generos/GenerosView.tsx'
import GeneroCadastroView from './views/generos/CadastroView.tsx'
import LayoutView from './views/LayoutView.tsx'
import GeneroLayuot from './views/generos/GenerosLayout.tsx'
import AutoresLayout from './views/autores/AutoresLayout.tsx'
import AutoresViews from './views/autores/AutoresView.tsx'
import AutorCadastroView from './views/autores/CadastroView.tsx'
import LivrosLayout from './views/livros/LivrosLayout.tsx'
import LivroCadastroView from './views/livros/CadastroView.tsx'
import LivrosViews from './views/livros/LivrosView.tsx'

function App() {


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

          <Route path='livros' element={<LivrosLayout />} >
            <Route index element={<LivrosViews />} />
            <Route path='cadastro/:livroId' element={<LivroCadastroView />} />
          </Route>


        </Route>
      </Routes>
      
    </BrowserRouter>




  )
}

export default App
