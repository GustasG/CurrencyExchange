import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

import config from "../../config";
import ExchangeRateRequest from "../../models/exchangeRateRequest";

export const exchangeApi = createApi({
  reducerPath: "exchangeApi",
  keepUnusedDataFor: 60 * 60,
  baseQuery: fetchBaseQuery({
    baseUrl: `${config.ServerBaseUrl}/Exchange/`
  }),
  endpoints: (builder) => ({
    getCurrencies: builder.query<string[], void>({
      query: () => "Currencies"
    }),
    getExchangeRate: builder.mutation<number, ExchangeRateRequest>({
      query: (body) => ({
        url: "Rate",
        method: "POST",
        body: body
      })
    })
  })
});

export const { useGetCurrenciesQuery, useGetExchangeRateMutation } = exchangeApi; 