import axios from 'axios';

// Centralisera API URL senare så att vi slipper deffiniera det i varje fil
const API_URL = 'http://localhost:5276/api/Articles';

// Denna funkar om man bara vill filtrera på sentiment, men de skiter sig när man blandar in arrayer
// Får konstruera mitt query manuellt istället zzzz
/* export const getFilteredArticles = async (query) => {
    const response = await axios.get(`${API_URL}`, {
        params: query,
    });
    return response.data;
}; */

export const getFilteredArticles = async (query) => {
    const params = new URLSearchParams();

    if (query.minSentiment !== undefined) {
        params.append("minSentiment", query.minSentiment);
    }

    if (query.categories && query.categories.length > 0) {
        query.categories.forEach((cat) => {
            params.append("categories", cat);
        });
    }

    const response = await axios.get(`${API_URL}?${params.toString()}`);
    return response.data;
};

export const getArticleById = async (id) => {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
};

export const createArticle = async (article) => {
    const response = await axios.post(API_URL, article);
    return response.data;
};

export const updateArticle = async (id, article) => {
    const response = await axios.put(`${API_URL}/${id}`, article);
    return response.data;
};

export const deleteArticle = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
};
