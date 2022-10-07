import { configureStore } from "@reduxjs/toolkit";

import { exchangeSlice } from "./slices/exchangeSlice";
import { historicalExchangeSlice } from "./slices/historicalExcangeSlice";

import { exchangeApi } from "./api/exchangeApi";
import { historicalExchangeApi } from "./api/historicalExchangeApi";

const store = configureStore({
  reducer: {
    [exchangeApi.reducerPath]: exchangeApi.reducer,
    [historicalExchangeApi.reducerPath]: historicalExchangeApi.reducer,

    [exchangeSlice.name]: exchangeSlice.reducer,
    [historicalExchangeSlice.name]: historicalExchangeSlice.reducer
  },
  middleware: (currentMiddleware) =>
    currentMiddleware()
      .concat(exchangeApi.middleware)
      .concat(historicalExchangeApi.middleware)
});

export default store;
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;