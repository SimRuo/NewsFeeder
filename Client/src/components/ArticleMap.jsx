import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";

// Fix marker icons (Vite can't resolve them otherwise)
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: new URL("leaflet/dist/images/marker-icon-2x.png", import.meta.url),
  iconUrl: new URL("leaflet/dist/images/marker-icon.png", import.meta.url),
  shadowUrl: new URL("leaflet/dist/images/marker-shadow.png", import.meta.url),
});

const ArticleMap = ({ articles }) => {
  const defaultCenter = [59.3293, 18.0686]; // Stockholm

  return (
    <MapContainer center={defaultCenter} zoom={5} style={{ height: "100vh", width: "100%" }}>
      <TileLayer attribution="&copy; OpenStreetMap contributors" url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
      {articles.map((article) => (
        <Marker key={article.id} position={[article.latitude, article.longitude]}>
          <Popup>
            <strong>{article.title}</strong>
            <br />
            <a href={article.url} target="_blank">
              Read more
            </a>
            <br />
            Sentiment: {article.sentiment.toFixed(2)}
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default ArticleMap;
