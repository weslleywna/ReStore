CREATE EXTENSION IF NOT EXISTS "uuid-ossp" SCHEMA public;

CREATE TABLE IF NOT EXISTS basket_items (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    basket_id UUID NOT NULL,
    product_id UUID NOT NULL,
    quantity INT NOT NULL,
    CONSTRAINT fk_product FOREIGN KEY (product_id) REFERENCES products (id) ON DELETE CASCADE,  
    CONSTRAINT fk_basket FOREIGN KEY (basket_id) REFERENCES baskets (id) ON DELETE CASCADE
);

CREATE TRIGGER update_timestamp_trigger
BEFORE UPDATE ON basket_items
FOR EACH ROW
EXECUTE FUNCTION update_timestamp();