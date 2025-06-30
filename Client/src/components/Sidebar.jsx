import { Box, Typography, Divider, Slider, InputLabel, Select, MenuItem, OutlinedInput, Chip } from "@mui/material";

const categoryOptions = ["Politics", "Sports", "Technology", "Weather"];

const Sidebar = ({ filters, onFilterChange }) => {
  const handleSentimentChange = (_, newValue) => {
    onFilterChange({ ...filters, minSentiment: newValue });
  };

  const handleCategoriesChange = (e) => {
    const {
      target: { value },
    } = e;
    onFilterChange({
      ...filters,
      categories: typeof value === "string" ? value.split(",") : value,
    });
  };

  return (
    <Box sx={{ p: 2, minWidth: 240 }}>
      <Typography variant="h6">Newsfeeder</Typography>
      <Divider sx={{ my: 2 }} />

      <Typography gutterBottom>Sentiment Filter</Typography>
      <Slider value={filters.minSentiment} onChange={handleSentimentChange} step={0.1} min={-1} max={1} valueLabelDisplay="auto" />

      <Divider sx={{ my: 2 }} />

      <InputLabel id="category-multiselect-label">Categories</InputLabel>
      <Select
        labelId="category-multiselect-label"
        multiple
        value={filters.categories}
        onChange={handleCategoriesChange}
        input={<OutlinedInput label="Categories" />}
        renderValue={(selected) => (
          <Box sx={{ display: "flex", flexDirection: "column", gap: 0.5 }}>
            {selected.map((value) => (
              <Chip key={value} label={value} />
            ))}
          </Box>
        )}
        fullWidth
        MenuProps={{
          anchorOrigin: {
            vertical: "top",
            horizontal: "right",
          },
          transformOrigin: {
            vertical: "top",
            horizontal: "left",
          },
        }}
      >
        {categoryOptions.map((cat) => (
          <MenuItem key={cat} value={cat}>
            {cat}
          </MenuItem>
        ))}
      </Select>
    </Box>
  );
};

export default Sidebar;
