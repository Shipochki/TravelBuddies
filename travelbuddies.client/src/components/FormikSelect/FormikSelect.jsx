import { FormControl, InputLabel, Select } from "@mui/material";

export const FormikSelect = ({ field, form, label, children, ...props }) => (
    <FormControl fullWidth>
      <InputLabel>{label}</InputLabel>
      <Select
        {...field}
        {...props}
        error={Boolean(form.errors[field.name] && form.touched[field.name])}
        sx={{
            width: '12vw'
        }}
        variant="standard"
      >
        {children}
      </Select>
      {form.errors[field.name] && form.touched[field.name] && (
        <div style={{ color: 'red', fontSize: '12px' }}>{form.errors[field.name]}</div>
      )}
    </FormControl>
  );