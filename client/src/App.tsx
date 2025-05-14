import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { Notifications } from "./components/ui/notification/Notification";
import { ReservationList } from "./components/ui/reservation/ReservationList";
import { ReservationPage } from "./pages/reservation/ReservationPage";
import { ReservationListPage } from "./pages/reservation/ReservationListPage";
import ReservationDetailPage from "./pages/reservation/ReservationDetailPage";
import { Provider } from "react-redux";
import { store } from "./store";

function App() {
  const [count, setCount] = useState(0);

  return (
    <Provider store={store}>
      <BrowserRouter>
        <Notifications />
        <Routes>
          <Route path="/" element={<ReservationPage />} />
          <Route path="/list" element={<ReservationListPage />} />
          <Route path="/detail" element={<ReservationDetailPage />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}

export default App;

// todo: 다 준비됐는데, tailwind css 적용 안돼서 확인해봐야함.