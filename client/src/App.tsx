import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import { Route, Routes } from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { Notifications } from "./components/ui/notification/Notification";
import { ReservationList } from "./components/reservation/ReservationList";
import { ReservationPage } from "./pages/reservation/ReservationPage";
import { ReservationListPage } from "./pages/reservation/ReservationListPage";
import ReservationDetailPage from "./pages/reservation/ReservationDetailPage";
import { Provider } from "react-redux";
import { store } from "./store";
import { ProtectedRoute } from "./pages/auth/ProtectedRoute";
import { LoginPage } from "./pages/auth/LoginPage";
import { Dashboard } from "./components/ui/admin/Dashboard";

const CenteredLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="flex flex-col  min-h-screen ">
      <div className="container mx-auto px-4 py-8 md:py-12">
        <div className="max-w-3xl mx-auto">{children}</div>
      </div>
    </div>
  );
};

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Notifications />
        <CenteredLayout>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route
              path="/admin/*"
              element={
                <ProtectedRoute requiredRole="admin">
                  {/* <AdminLayout> */}
                  <Routes>
                    <Route path="/dashboard" element={<Dashboard />} />
                    {/* <Route
                        path="/store-hours"
                        element={<StoreHoursManagement />}
                      /> */}
                  </Routes>
                  {/* </AdminLayout> */}
                </ProtectedRoute>
              }
            />
            <Route path="/" element={<ReservationPage />} />
            <Route path="/list" element={<ReservationListPage />} />
            <Route path="/detail" element={<ReservationDetailPage />} />
          </Routes>
        </CenteredLayout>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
