# StorySpoiler Web App Test Automation

This folder contains automated test scripts for the **StorySpoiler Web App**, a platform for sharing and managing story spoilers. The project focuses on validating front-end functionality through Selenium IDE and WebDriver tests.

## Key Features Tested

### 1. Home Page
- Navigation menu for unregistered users with "LOG IN" and "SIGN UP" options.
- Interactive sections like "Summarize the Story," "Upload a Picture," and "Ready to Spoil a Story?"

### 2. User Authentication
- **Sign Up**:
  - Validates user registration with Username, Email, Password, and other details.
- **Log In**:
  - Confirms correct login behavior with registered user credentials.
  - Ensures proper redirection and display of user-specific features.

### 3. Spoiler Management
- **Create Spoilers**:
  - Tests adding spoilers with valid and invalid data.
  - Verifies spoilers appear correctly on the Home Page.
- **Edit Spoilers**:
  - Modifies details of existing spoilers and asserts changes.
- **Delete Spoilers**:
  - Validates successful deletion and ensures removed spoilers no longer appear.

### 4. Negative Scenarios
- Attempting to edit or delete non-existent spoilers.
- Handling invalid or missing data during spoiler creation.

## Automated Testing Highlights

### Tools & Frameworks
- **Selenium IDE**:
  - Recorded tests for basic navigation and user flows.
- **Selenium WebDriver**:
  - Advanced test automation with custom scripts using C# and NUnit.
  - Integration of dynamic locators, explicit waits, and user interaction handling.
- **ChromeDriver**:
  - Browser interaction for executing automated tests.

### Example Test Scenarios
1. **Home Page Navigation**:
   - Verify the presence of navigation options for unregistered users.
   - Assert redirection and messaging for logged-in users.
2. **Create and Manage Spoilers**:
   - Add, edit, and delete spoilers, confirming proper UI updates.
3. **Error Handling**:
   - Assert appropriate error messages for invalid actions like editing non-existent spoilers.

## Why This Project?
This project demonstrates the application of front-end test automation techniques to validate critical workflows and edge cases in a user-centric web application. It showcases:
- Expertise in Selenium for UI testing.
- Structured and reusable test scripts.
- A focus on user experience validation and error handling.

---