document.addEventListener('DOMContentLoaded', async () => {
    const functionApiUrl = 'https://counter1.azurewebsites.net/api/getandupdatecounter?code=IaE6gG6-VMf5-OoYij0GoU6frsWafqmA79OP2mfX5U_HAzFuBTP1bQ%3D%3D';

    try {
        const response = await fetch(functionApiUrl);
        if (!response.ok) {
            throw new Error(`Failed to fetch data. Status: ${response.status}`);
        }

        const data = await response.json();
        console.log("Website called function API.");
        const count = data.count;
        document.getElementById("counter").innerText = count;
    } catch (error) {
        console.error(error);
    }
});
