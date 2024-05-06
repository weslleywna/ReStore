import { useEffect, useState } from "react"
import { Product } from "./models/product";

function App() {

  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    fetch('http://localhost:5000/api/products')
      .then(response => response.json())
      .then(data => setProducts(data))
  }, []);

  return (
    <div>
      <h2>ReStore</h2>
      <ul>
        {products.map((product, index) => {
          return <li key={index}> {product.name} {product.price}</li>
        })}
      </ul>
    </div>
  )
}

export default App
