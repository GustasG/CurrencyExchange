import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

import config from "../../config";
import { HistoricalExchangeCurrencyRequest, HistoricalExchangeRateRequest } from "../../models/historicalExchangeRequest";

export const historicalExchangeApi = createApi({
  reducerPath: "historicalExchangeApi",
  keepUnusedDataFor: 60 * 60,
  baseQuery: fetchBaseQuery({
    baseUrl: `${config.ServerBaseUrl}/HistoricalExchange`
  }),
  endpoints: (builder) => ({
    getDates: builder.query<string[], void>({
      query: () => "Dates",
      transformResponse: (dates: string[]) => dates.sort()
    }),
    getCurrencies: builder.mutation<string[], HistoricalExchangeCurrencyRequest>({
      query: (body) => ({
        url: "Currencies",
        method: "POST",
        body: body
      })
    }),
    getExchangeRate: builder.mutation<number, HistoricalExchangeRateRequest>({
      query: (body) => ({
        url: "Rate",
        method: "POST",
        body: body
      })
    })
  })
});

export const { useGetDatesQuery, useGetCurrenciesMutation, useGetExchangeRateMutation } = historicalExchangeApi;