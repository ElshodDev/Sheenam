//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.JSInterop;

namespace Sheenam.Blazor.Services
{
    public class TokenStorageService
    {
        private readonly IJSRuntime jsRuntime;
        private const string TOKEN_KEY = "authToken";
        private string memoryToken = null;  // ✅ Fallback:  memory storage

        public TokenStorageService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
        {
            try
            {
                // ✅ Prerendering paytida memory'dan qaytarish
                if (!IsJavaScriptAvailable())
                {
                    Console.WriteLine("⚠️ [TokenStorage] JSInterop not available, using memory token");
                    return memoryToken;
                }

                var token = await jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);

                if (!string.IsNullOrEmpty(token))
                {
                    memoryToken = token;  // ✅ Memory'ga ham saqlash
                    Console.WriteLine($"🔑 [TokenStorage] Token retrieved from localStorage (length: {token.Length})");
                }
                else
                {
                    Console.WriteLine("⚠️ [TokenStorage] No token found in localStorage");
                }

                return token;
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("JavaScript interop"))
            {
                Console.WriteLine($"⚠️ [TokenStorage] Prerendering mode - using memory token");
                return memoryToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [TokenStorage] Error getting token: {ex.Message}");
                return memoryToken;  // ✅ Fallback
            }
        }

        public async Task SetTokenAsync(string token)
        {
            try
            {
                memoryToken = token;  // ✅ Memory'ga saqlash (har doim)

                // ✅ Prerendering paytida faqat memory
                if (!IsJavaScriptAvailable())
                {
                    Console.WriteLine($"⚠️ [TokenStorage] JSInterop not available, token saved to memory only");
                    return;
                }

                await jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);
                Console.WriteLine($"✅ [TokenStorage] Token saved to localStorage & memory (length: {token?.Length ??  0})");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("JavaScript interop"))
            {
                Console.WriteLine($"⚠️ [TokenStorage] Prerendering mode - token saved to memory only");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [TokenStorage] Error saving token: {ex.Message}");
            }
        }

        public async Task RemoveTokenAsync()
        {
            try
            {
                memoryToken = null;  // ✅ Memory'dan o'chirish

                if (!IsJavaScriptAvailable())
                {
                    Console.WriteLine("⚠️ [TokenStorage] JSInterop not available, token removed from memory only");
                    return;
                }

                await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
                Console.WriteLine("✅ [TokenStorage] Token removed from localStorage & memory");
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("JavaScript interop"))
            {
                Console.WriteLine("⚠️ [TokenStorage] Prerendering mode - token removed from memory only");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [TokenStorage] Error removing token: {ex.Message}");
            }
        }

        private bool IsJavaScriptAvailable()
        {
            // ✅ JSInterop mavjudligini tekshirish
            try
            {
                return jsRuntime is IJSInProcessRuntime;
            }
            catch
            {
                return false;
            }
        }
    }
}