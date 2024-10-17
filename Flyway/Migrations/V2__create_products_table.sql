CREATE EXTENSION IF NOT EXISTS "uuid-ossp" SCHEMA public;

CREATE TABLE IF NOT EXISTS products (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    brand VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    type VARCHAR(255) NOT NULL,
    price BIGINT NOT NULL,
    quantity_in_stock INT NOT NULL,
    description TEXT,
    picture_url TEXT
);

CREATE TRIGGER update_timestamp_trigger
BEFORE UPDATE ON products
FOR EACH ROW
EXECUTE FUNCTION update_timestamp();
