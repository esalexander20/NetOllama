const chatContainer = document.getElementById('chat-container');
const promptInput = document.getElementById('prompt-input');
const sendButton = document.querySelector('button');

function addMessage(content, isUser) {
    const messageDiv = document.createElement('div');
    messageDiv.className = `message ${isUser ? 'user-message' : 'bot-message'}`;
    messageDiv.textContent = content;
    chatContainer.appendChild(messageDiv);
    chatContainer.scrollTop = chatContainer.scrollHeight;
}

async function sendMessage() {
    const prompt = promptInput.value.trim();
    if (!prompt) return;

    // Disable input and button while processing
    promptInput.disabled = true;
    sendButton.disabled = true;

    try {
        addMessage(prompt, true);
        promptInput.value = '';

        const response = await fetch('/api/chat', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                model: 'mistral:latest',
                prompt: prompt,
                stream: false
            })
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        addMessage(data.response, false);
    } catch (error) {
        console.error('Error:', error);
        addMessage('Error: Failed to get response from the server', false);
    } finally {
        // Re-enable input and button
        promptInput.disabled = false;
        sendButton.disabled = false;
        promptInput.focus();
    }
}

// Event listeners
promptInput.addEventListener('keypress', (e) => {
    if (e.key === 'Enter' && !e.shiftKey && !promptInput.disabled) {
        e.preventDefault();
        sendMessage();
    }
});

// Initial focus
promptInput.focus();
