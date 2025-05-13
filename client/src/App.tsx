import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Routes } from 'react-router-dom'
import { BrowserRouter } from 'react-router-dom'
import { Notifications } from './components/ui/notification/Notification'

function App() {
  const [count, setCount] = useState(0)

  return (
    <BrowserRouter>
      <Notifications />
      <Routes>
        {/* <Route path="/" element={<Home />} /> */}
      </Routes>
    </BrowserRouter>
  );
}

export default App
