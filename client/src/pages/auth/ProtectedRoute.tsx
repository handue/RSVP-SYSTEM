// src/components/auth/ProtectedRoute.tsx
import { Navigate } from 'react-router-dom';
import { useAppSelector } from '../../hooks/useAppSelector';

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRole?: 'Admin';
}

export const ProtectedRoute = ({ children, requiredRole }: ProtectedRouteProps) => {
  const { user, status } = useAppSelector((state) => state.auth);

  if (status === 'loading') {
    return <div>Loading...</div>;
  }

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  if (requiredRole && user.role !== requiredRole) {
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
};