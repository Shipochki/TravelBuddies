import { TextField } from "@mui/material";
import { useField } from "formik";

export const FormikTextField = ({ label, isRequired, ...props }) => {
    const [field, meta] = useField(props);
    return(
  <TextField
    {...field}
    {...props}
    label={label}
    required={isRequired}
    error={meta.touched && Boolean(meta.error)}
    helperText={meta.touched && meta.error}
    margin="normal"
    sx={{
        maxWidth:"200px"
    }}
  />)
}