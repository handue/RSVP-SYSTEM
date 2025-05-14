// custom UI component
interface Step {
    title: string;
    status: 'complete' | 'current' | 'upcoming';
  }
  
  interface StepsProps {
    steps: Step[];
  }
  
  export const Steps = ({ steps }: StepsProps) => {
    return (
      <div className="flex items-center justify-between">
        {steps.map((step, index) => (
          <div key={step.title} className="flex items-center">
            <div className={`flex items-center justify-center w-8 h-8 rounded-full
              ${step.status === 'complete' ? 'bg-green-500' : 
                step.status === 'current' ? 'bg-blue-500' : 'bg-gray-200'}`}>
              {step.status === 'complete' ? '✓' : index + 1}
            </div>
            <span className="ml-2">{step.title}</span>
            {index < steps.length - 1 && (
              <div className="w-24 h-0.5 mx-4 bg-gray-200" />
            )}
          </div>
        ))}
      </div>
    );
  };