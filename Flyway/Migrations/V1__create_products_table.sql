CREATE TABLE products (
    id UUID PRIMARY KEY,
    brand VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    type VARCHAR(255) NOT NULL,
    price BIGINT NOT NULL,
    quantity_in_stock INT NOT NULL,
    description TEXT,
    picture_url TEXT
);