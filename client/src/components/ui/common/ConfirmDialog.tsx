import React, { createContext, useContext, useState } from "react";

interface ConfirmDialogOptions {
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
  variant?: "default" | "destructive";
  onConfirm: () => void | Promise<void>;
  onCancel?: () => void;
}

interface ConfirmDialogContextType {
  showConfirm: (options: ConfirmDialogOptions) => void;
}

const ConfirmDialogContext = createContext<ConfirmDialogContextType | null>(
  null
);

interface ButtonProps {
  children: React.ReactNode;
  variant?: "default" | "destructive" | "outline";
  onClick?: () => void;
  disabled?: boolean;
  className?: string;
}

const Button: React.FC<ButtonProps> = ({
  children,
  variant = "default",
  onClick,
  disabled = false,
  className = "",
}) => {
  const baseClasses =
    "px-4 py-2 rounded-lg font-medium transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 disabled:opacity-50 disabled:cursor-not-allowed";

  const variants = {
    default:
      "bg-indigo-600 text-white hover:bg-indigo-700 focus:ring-indigo-500",
    destructive: "bg-red-600 text-white hover:bg-red-700 focus:ring-red-500",
    outline:
      "border border-gray-300 text-gray-700 hover:bg-gray-50 focus:ring-indigo-500",
  };

  return (
    <button
      className={`${baseClasses} ${variants[variant]} ${className}`}
      onClick={onClick}
      disabled={disabled}
    >
      {children}
    </button>
  );
};

interface ConfirmDialogProps {
  isOpen: boolean;
  title: string;
  message: string;
  confirmText: string;
  cancelText: string;
  variant: "default" | "destructive";
  onConfirm: () => void;
  onCancel: () => void;
  loading?: boolean;
}

const ConfirmDialog: React.FC<ConfirmDialogProps> = ({
  isOpen,
  title,
  message,
  confirmText,
  cancelText,
  variant,
  onConfirm,
  onCancel,
  loading = false,
}) => {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4">
      {/* Backdrop */}
      <div
        className="fixed inset-0 bg-black/50 transition-opacity backdrop-blur-sm"
        onClick={onCancel}
      />

      {/* Dialog */}
      <div className="relative bg-white rounded-xl shadow-2xl max-w-md w-full p-6 transform transition-all duration-200 scale-100">
        {/* Icon based on variant */}
        <div className="flex items-center mb-4">
          {variant === "destructive" ? (
            <div className="flex-shrink-0 w-10 h-10 bg-red-100 rounded-full flex items-center justify-center mr-3">
              <svg
                className="w-6 h-6 text-red-600"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-2.5L13.732 4c-.77-.833-1.964-.833-2.732 0L3.084 16.5c-.77.833.192 2.5 1.732 2.5z"
                />
              </svg>
            </div>
          ) : (
            <div className="flex-shrink-0 w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center mr-3">
              <svg
                className="w-6 h-6 text-blue-600"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                />
              </svg>
            </div>
          )}
          <h3 className="text-lg font-semibold text-gray-900">{title}</h3>
        </div>

        <p className="text-gray-600 mb-6 leading-relaxed">{message}</p>

        <div className="flex gap-3 justify-end">
          <Button variant="outline" onClick={onCancel} disabled={loading}>
            {cancelText}
          </Button>
          <Button
            variant={variant === "destructive" ? "destructive" : "default"}
            onClick={onConfirm}
            disabled={loading}
          >
            {loading ? (
              <div className="flex items-center">
                <svg
                  className="animate-spin -ml-1 mr-2 h-4 w-4"
                  fill="none"
                  viewBox="0 0 24 24"
                >
                  <circle
                    className="opacity-25"
                    cx="12"
                    cy="12"
                    r="10"
                    stroke="currentColor"
                    strokeWidth="4"
                  ></circle>
                  <path
                    className="opacity-75"
                    fill="currentColor"
                    d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
                  ></path>
                </svg>
                Processing...
              </div>
            ) : (
              confirmText
            )}
          </Button>
        </div>
      </div>
    </div>
  );
};

let globalSetDialog: React.Dispatch<
  React.SetStateAction<ConfirmDialogOptions | null>
> | null = null;
let globalSetLoading: React.Dispatch<React.SetStateAction<boolean>> | null =
  null;

export const showConfirm = (options: ConfirmDialogOptions) => {
  if (globalSetDialog) {
    globalSetDialog(options);
  }
};

export const ConfirmDialogProvider: React.FC = () => {
  const [dialog, setDialog] = useState<ConfirmDialogOptions | null>(null);
  const [loading, setLoading] = useState(false);

  React.useEffect(() => {
    globalSetDialog = setDialog;
    globalSetLoading = setLoading;

    return () => {
      globalSetDialog = null;
      globalSetLoading = null;
    };
  }, []);

  const handleConfirm = async () => {
    if (!dialog) return;

    try {
      setLoading(true);
      await dialog.onConfirm();
      setDialog(null);
    } catch (error) {
      console.error("Confirm action failed:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = () => {
    if (dialog?.onCancel) {
      dialog.onCancel();
    }
    setDialog(null);
  };

  return (
    <ConfirmDialog
      isOpen={!!dialog}
      title={dialog?.title || ""}
      message={dialog?.message || ""}
      confirmText={dialog?.confirmText || "Confirm"}
      cancelText={dialog?.cancelText || "Cancel"}
      variant={dialog?.variant || "default"}
      onConfirm={handleConfirm}
      onCancel={handleCancel}
      loading={loading}
    />
  );
};

export const useConfirmDialog = () => {
  return { showConfirm };
};
