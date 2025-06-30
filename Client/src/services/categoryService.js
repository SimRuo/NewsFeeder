import axios from 'axios';


export const getCategories = async () => {
    const response = await axios.get(`${API_URL}`);
    return response.data;
};