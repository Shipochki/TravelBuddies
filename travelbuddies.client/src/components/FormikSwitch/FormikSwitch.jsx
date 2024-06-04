import React from 'react';
import { useField } from 'formik';
import { FormControlLabel, Switch } from '@mui/material';

export const FormikSwitch = ({ label, ...props }) => {
  const [field] = useField(props);
  return (
    <FormControlLabel
      control={<Switch {...field} checked={field.value} />}
      label={label}
    />
  );
};