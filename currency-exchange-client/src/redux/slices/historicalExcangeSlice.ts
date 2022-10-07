import { createSlice, PayloadAction } from "@reduxjs/toolkit";

import { dateToIsoDateOnlyString } from "../../util";

export interface HistoricalExchangeState {
  date: string,
  currencyFrom: string,
  currencyTo: string,
  amount: number,
  rate: number
};

export const historicalExchangeSlice = createSlice({
  name: "historicalExchange",
  initialState: {
    date: dateToIsoDateOnlyString(new Date()),
    currencyFrom: "",
    currencyTo: "",
    amount: 0,
    rate: 0
  },
  reducers: {
    setDate: (state, action: PayloadAction<string>) => {
      state.date = action.payload;
    },
    setCurrencyFrom: (state, action: PayloadAction<string>) => {
      state.currencyFrom = action.payload;
    },
    setCurrencyTo: (state, action: PayloadAction<string>) => {
      state.currencyTo = action.payload;
    },
    setAmount: (state, action: PayloadAction<number>) => {
      state.amount = action.payload;
    },
    setRate: (state, action: PayloadAction<number>) => {
      state.rate = action.payload;
    }
  }
});

export const { setDate, setCurrencyFrom, setCurrencyTo, setAmount, setRate } = historicalExchangeSlice.actions;