// custom UI component
interface Step {
  title: string;
  status: "complete" | "current" | "upcoming";
}

interface StepsProps {
  steps: Step[];
}

export const Steps = ({ steps }: StepsProps) => {
  const colors = {
    primary: "bg-indigo-600",
    primaryText: "text-white",
    primaryLight: "bg-indigo-100",
    primaryBorder: "border-indigo-600",
    complete: "bg-emerald-500",
    completeText: "text-white",
    upcoming: "bg-gray-100",
    upcomingText: "text-gray-500",
    connector: "bg-gray-200",
    connectorActive: "bg-green-100",
  };

  return (
    <div className="flex items-center justify-between w-full">
      {steps.map((step, index) => (
        <div
          key={step.title}
          className={`flex items-center ${
            index === steps.length - 1 ? "flex-[0.3]" : "flex-1"
          }`}
        >
          <div
            className={`
              flex items-center justify-center w-10 h-10 rounded-full font-medium text-sm
              transition-all duration-200 ease-in-out shadow-sm
              ${
                step.status === "complete"
                  ? `${colors.complete} ${colors.completeText}`
                  : step.status === "current"
                  ? `${colors.primary} ${colors.primaryText}`
                  : `${colors.upcoming} ${colors.upcomingText} border ${colors.primaryBorder}`
              }
            `}
          >
            {step.status === "complete" ? (
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="h-5 w-5"
                viewBox="0 0 20 20"
                fill="currentColor"
              >
                <path
                  fillRule="evenodd"
                  d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 111.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z"
                  clipRule="evenodd"
                />
              </svg>
            ) : (
              index + 1
            )}
          </div>
          <span
            className={`ml-3 font-medium text-sm ${
              step.status !== "complete" ? "text-gray-500" : "text-gray-900"
            }`}
          >
            {step.title}
          </span>
          {index < steps.length - 1 && (
            <div className="flex-1 mx-4">
              <div
                className={`h-1 ${
                  step.status === "complete"
                    ? colors.connectorActive
                    : colors.connector
                }`}
              />
            </div>
          )}
        </div>
      ))}
    </div>
  );
};
