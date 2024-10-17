CREATE EXTENSION IF NOT EXISTS "uuid-ossp" SCHEMA public;

CREATE TABLE IF NOT EXISTS baskets (
    id UUID DEFAULT uuid_generate_v4() PRIMARY KEY,
    buyer_id UUID NULL
);

CREATE TRIGGER update_timestamp_trigger
BEFORE UPDATE ON baskets
FOR EACH ROW
EXECUTE FUNCTION update_timestamp();