import { Box, Button, FormHelperText, Input } from "@mui/material";
import { useField, useFormikContext } from "formik";
import { useState } from "react";
import CloudUploadIcon from '@mui/icons-material/CloudUpload';

export const FormikFileInput = ({ label, ...props }) => {
  const { setFieldValue } = useFormikContext();
  const [field, meta] = useField(props);
  const [preview, setPreview] = useState(null);

  const handleFileChange = (event) => {
    setFieldValue(field.name, event.currentTarget.files[0]);
    if (event.currentTarget.files[0]) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setPreview(reader.result);
      };
      reader.readAsDataURL(event.currentTarget.files[0]);
    }
  };

  return (
    <Box
      margin="normal"
      display="flex"
      flexDirection="column"
      justifyContent="center"
      sx={{ width: "500px", height: "300px" }}
    >
      {preview && (
        <img
          src={preview}
          alt="Preview"
          style={{
            width: "200px",
            height: "200px",
            marginTop: "10px",
            borderRadius: "100px",
            objectFit: "cover",
          }}
        />
      )}
      {/* <Input
        type="file"
        onChange={(event) => {
          setFieldValue(field.name, event.currentTarget.files[0]);
          if (event.currentTarget.files[0]) {
            const reader = new FileReader();
            reader.onloadend = () => {
              setPreview(reader.result);
            };
            reader.readAsDataURL(event.currentTarget.files[0]);
          }
        }}
        inputProps={{ accept: "image/*" }}
        sx={{
          maxWidth: "200px",
        }}
      /> */}
      <Input
        type="file"
        inputProps={{ accept: 'image/*' }}
        onChange={handleFileChange}
        style={{ display: 'none' }}
        id="file-input"
      />
      <label htmlFor="file-input">
        <Button
          variant="contained"
          component="span"
          sx={{ mt: 2 }}
          startIcon={<CloudUploadIcon />}
        >
          Upload
        </Button>
      </label>
      {meta.touched && meta.error && (
        <FormHelperText error>{meta.error}</FormHelperText>
      )}
    </Box>
  );
};
