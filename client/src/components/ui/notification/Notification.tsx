// components/common/Notifications.tsx
import { useEffect } from "react";
import { useAppDispatch } from "../../../hooks/useAppDispatch";
import { useAppSelector } from "../../../hooks/useAppSelector";
import {
  selectNotifications,
  removeNotification,
} from "../../../store/notificationSlice";
import {
  X,
  CheckCircle,
  AlertCircle,
  Info,
  AlertTriangle,
  Loader2,
} from "lucide-react";

export function Notifications() {
  const notifications = useAppSelector(selectNotifications);
  const dispatch = useAppDispatch();

  // 자동으로 알림 제거
  useEffect(() => {
    notifications.forEach((notification) => {
      const timer = setTimeout(() => {
        dispatch(removeNotification(notification.id));
      }, notification.duration || 5000);

      return () => clearTimeout(timer);
    });
  }, [notifications, dispatch]);

  if (notifications.length === 0) return null;

  const getIcon = (type: string) => {
    switch (type) {
      case "success":
        return <CheckCircle className="h-5 w-5 text-green-500" />;

      case "error":
        return <AlertCircle className="h-5 w-5 text-red-500" />;
      case "warning":
        return <AlertTriangle className="h-5 w-5 text-yellow-500" />;
      case "info":
      default:
        return <Info className="h-5 w-5 text-blue-500" />;
    }
  };

  const getBackgroundColor = (type: string) => {
    switch (type) {
      case "success":
        return "bg-green-50 border-green-200";

      case "error":
        return "bg-red-50 border-red-200";
      case "warning":
        return "bg-yellow-50 border-yellow-200";
      case "info":
      default:
        return "bg-blue-50 border-blue-200";
    }
  };

  return (
    <div className="fixed top-4 right-4 z-50 space-y-2 max-w-[90vw] md:max-w-md">
      {notifications.map((notification) => (
        <div
          key={notification.id}
          className={`flex items-start p-3 md:p-4 rounded-md shadow-md border ${getBackgroundColor(
            notification.type
          )} animate-in slide-in-from-right`}
        >
          <div className="flex-shrink-0 mr-2 md:mr-3">{getIcon(notification.type)}</div>
          <div className="flex-1 mr-2 text-xs md:text-sm">
            <p className="text-gray-800">{notification.message}</p>
          </div>
          <button
            onClick={() => dispatch(removeNotification(notification.id))}
            className="flex-shrink-0 text-gray-400 hover:text-gray-600"
          >
            <X className="h-3 w-3 md:h-4 md:w-4" />
          </button>
        </div>
      ))}
    </div>
  );
}
