import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "../common/button";
import { Card, CardContent, CardHeader, CardTitle } from "../common/card";
import { AppDispatch, RootState } from "../../../store";
import { fetchDashboardData, confirmReservation, cancelReservation } from "../../../store/dashboardSlice";
import { useNotification } from "../../../hooks/useNotification";

export const Dashboard = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { showSuccess, showError } = useNotification();
  const { stats, todayReservations, status, error } = useSelector(
    (state: RootState) => state.dashboard
  );

  useEffect(() => {
    dispatch(fetchDashboardData());
  }, [dispatch]);

  const handleConfirm = async (id: string) => {
    try {
      await dispatch(confirmReservation(id)).unwrap();
      showSuccess("Reservation confirmed successfully.");
    } catch (error) {
      showError("Reservation confirmation failed.");
    }
  };

  const handleCancel = async (id: string) => {
    try {
      await dispatch(cancelReservation(id)).unwrap();
      showSuccess("Reservation cancelled successfully.");
    } catch (error) {
      showError("Reservation cancellation failed.");
    }
  };

  if (status === "loading") {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">DashBoard</h1>
        <Button onClick={() => dispatch(fetchDashboardData())}>
          Refresh
        </Button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
        <Card>
          <CardHeader>
            <CardTitle>Total Reservations</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-2xl font-bold">{stats?.totalReservations || 0}</p>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Today's Reservations</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-2xl font-bold">{stats?.todayReservations || 0}</p>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Pending Reservations</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-2xl font-bold">{stats?.pendingReservations || 0}</p>
          </CardContent>
        </Card>
        <Card>
          <CardHeader>
            <CardTitle>Confirmed Reservations</CardTitle>
          </CardHeader>
          <CardContent>
            <p className="text-2xl font-bold">{stats?.confirmedReservations || 0}</p>
          </CardContent>
        </Card>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Today's Reservations List</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead>
                <tr>
                  <th className="text-left">Name</th>
                  <th className="text-left">Service</th>
                  <th className="text-left">Time</th>
                  <th className="text-left">Status</th>
                  <th className="text-left">Actions</th>
                </tr>
              </thead>
              <tbody>
                {todayReservations.map((reservation) => (
                  <tr key={reservation.id}>
                    <td>{reservation.name}</td>
                    <td>{reservation.service}</td>
                    <td>{reservation.time}</td>
                    <td>{reservation.status}</td>
                    <td>
                      <div className="space-x-2">
                        <Button
                          variant="outline"
                          size="sm"
                          onClick={() => handleConfirm(reservation.id)}
                          disabled={reservation.status === "confirmed"}
                        >
                          Confirm
                        </Button>
                        <Button
                          variant="outline"
                          size="sm"
                          onClick={() => handleCancel(reservation.id)}
                          disabled={reservation.status === "cancelled"}
                        >
                          Cancel
                        </Button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};