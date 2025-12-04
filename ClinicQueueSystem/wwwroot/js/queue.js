// Requires Microsoft SignalR browser client to be loaded before this script
// Exposes a small API to Blazor components for subscribing to queue update events
(function () {
    const state = {
        connection: null,
        started: false,
        onQueueUpdated: null
    };

    async function ensureStarted() {
        if (state.started) return;
        if (!window.signalR) {
            console.error("SignalR client library not found. Make sure to include it before queue.js");
            return;
        }
        state.connection = new signalR.HubConnectionBuilder()
            .withUrl("/hubs/queue")
            .withAutomaticReconnect()
            .build();

        state.connection.on("queueUpdated", () => {
            if (typeof state.onQueueUpdated === "function") {
                state.onQueueUpdated();
            }
            // Also dispatch a DOM event for generic listeners
            window.dispatchEvent(new CustomEvent("queue-updated"));
        });

        try {
            await state.connection.start();
            state.started = true;
        } catch (err) {
            console.error("Failed to start QueueHub connection:", err);
        }
    }

    window.QueueUpdates = {
        async register(dotNetRef) {
            await ensureStarted();
            state.onQueueUpdated = () => {
                try {
                    if (dotNetRef) {
                        dotNetRef.invokeMethodAsync("NotifyQueueUpdated");
                    }
                } catch (e) {
                    console.error("NotifyQueueUpdated failed:", e);
                }
            };
        },
        async dispose() {
            state.onQueueUpdated = null;
            if (state.connection && state.started) {
                try {
                    await state.connection.stop();
                } catch {
                    // ignore
                }
                state.started = false;
                state.connection = null;
            }
        }
    };
})();


