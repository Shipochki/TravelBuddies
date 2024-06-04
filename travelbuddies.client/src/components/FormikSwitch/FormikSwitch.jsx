import React from 'react';
import { useField } from 'formik';
import { FormControlLabel, Switch } from '@mui/material';

export const FormikSwitch = ({ field, form, label, ...props }) => {
  return (
    <FormControlLabel
    control={
      <Switch
        checked={field.value}
        onChange={(event) => form.setFieldValue(field.name, event.target.checked)}
        {...props}
      />
    }
    label={label}
  />
  );
};