import { Box, FormHelperText, Input } from "@mui/material";
import { useField } from "formik";

export const FormikFileInput = ({ label, setFieldValue, ...props }) => {
    const [field, meta] = useField(props);
    return (
      <Box margin="normal">
        <Input
          type="file"
          onChange={(event) => {
            setFieldValue(field.name, event.currentTarget.files[0]);
          }}
          inputProps={{ accept: 'image/*' }}
          fullWidth
        />
        {meta.touched && meta.error && (
          <FormHelperText error>{meta.error}</FormHelperText>
        )}
      </Box>
    );
  };