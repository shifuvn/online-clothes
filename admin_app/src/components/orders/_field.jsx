import React from "react";
import { Box, Step, StepButton, Stepper } from "@mui/material";
import { useRecordContext } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";
import { toast } from "react-toastify";

export const OrderStepperField = (props) => {
  const record = useRecordContext();
  const [activeStep, setActiveStep] = React.useState();

  const steps = ["Pending", "Delivering", "Success", "Canceled"];

  React.useEffect(() => {
    const getStartActiveStep = () => {
      steps.forEach((step, idx) => {
        if (step === record.state) {
          setActiveStep(idx);
        }
      });
    };

    getStartActiveStep();
  }, [record.state, steps]);

  const handleStep = (index) => async () => {
    let isSuccess;
    let result;
    switch (index) {
      case 1:
        result = await HttpApiProvider.put(`orders/${record.id}/delivery`);
        isSuccess = result?.status === 200;
        break;
      case 2:
        result = await HttpApiProvider.put(`orders/${record.id}/success`);
        isSuccess = result?.status === 200;
        break;
      case 3:
        result = await HttpApiProvider.put(`orders/${record.id}/cancel`);
        isSuccess = result?.status === 200;
        break;
      default:
        break;
    }
    if (isSuccess) {
      toast.success("Cập nhập thành công");
      setActiveStep(index);
      window.location.reload();
    }
  };

  return (
    <div style={{ padding: "32px" }}>
      <Box sx={{ width: "100%" }}>
        <Stepper nonLinear activeStep={activeStep}>
          {steps.map((label, idx) => (
            <Step key={label}>
              <StepButton onClick={handleStep(idx)}>{label}</StepButton>
            </Step>
          ))}
        </Stepper>
      </Box>
    </div>
  );
};
