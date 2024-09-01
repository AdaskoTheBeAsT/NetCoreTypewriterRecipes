#!/bin/sh

# Define the directory where the cache-busted file is located
CONFIG_DIR="/var/www"
BASENAME="env-config"

# Find the current cache-busted file (e.g., env-config.[hash].js)
CURRENT_FILE=$(ls $CONFIG_DIR/$BASENAME.*.js 2>/dev/null)

if [ -z "$CURRENT_FILE" ]; then
  echo "No existing cache-busted file found. Using default name."
  CACHE_BUSTED_FILE="$CONFIG_DIR/$BASENAME.js"
else
  echo "Found existing cache-busted file: $CURRENT_FILE"
  CACHE_BUSTED_FILE="$CURRENT_FILE"
fi

# Recreate config file
rm -rf "$CACHE_BUSTED_FILE"
touch "$CACHE_BUSTED_FILE"

# Start building the output in a variable
output="window._env_ = {\n"

# Initialize a variable to track whether this is the first entry
first_entry=true

# Process each line in the .env file
while IFS='=' read -r varname varvalue || [ -n "$varname" ]; do
  # Skip empty lines or lines that do not contain '='
  if [ -z "$varname" ] || [ -z "$varvalue" ]; then
    continue
  fi

  # Read the value of the current variable if it exists as an Environment variable
  value=$(eval echo \$$varname)

  # Otherwise, use the value from the .env file
  [ -z "$value" ] && value=$varvalue

  # Remove any newline or control characters from the value
  value=$(echo "$value" | tr -d '\n' | tr -d '\r')

  # Properly escape double quotes in the value
  value=$(printf '%s' "$value" | sed 's/"/\\"/g')

  # Build the key-value pair string
  if [ "$first_entry" = true ]; then
    output="$output  \"$varname\": \"$value\""
    first_entry=false
  else
    output="$output,\n  \"$varname\": \"$value\""
  fi

done <.env

# Close the JSON object in the output
output="$output\n}"

# Write the final content to the file
printf "$output\n" >"$CACHE_BUSTED_FILE"

echo "Generated $CACHE_BUSTED_FILE successfully."
