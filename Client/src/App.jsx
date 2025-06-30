import { useEffect, useState } from "react";
import { getFilteredArticles } from "./services/articleService";
import ArticleMap from "./components/ArticleMap.jsx";
import Sidebar from "./components/Sidebar";
import { Box } from "@mui/material";
function App() {
  const [articles, setArticles] = useState([]);
  const [filters, setFilters] = useState({
    minSentiment: -1,
    categories: [],
  });

  useEffect(() => {
    getFilteredArticles(filters).then(setArticles).catch(console.error);
  }, [filters]);

  return (
    <>
      <Box sx={{ display: "flex" }}>
        <Sidebar filters={filters} onFilterChange={setFilters} />
        <Box sx={{ flexGrow: 1 }}>
          <ArticleMap articles={articles} />
        </Box>
      </Box>
    </>
  );
}

export default App;
