import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface ExchangeState {
  currencyFrom: string,
  currencyTo: string,
  amount: number,
  rate: number
};

export const exchangeSlice = createSlice({
  name: "exchange",
  initialState: {
    currencyFrom: "",
    currencyTo: "",
    amount: 0.0,
    rate: 0.0
  },
  reducers: {
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

export const { setCurrencyFrom, setCurrencyTo, setAmount, setRate } = exchangeSlice.actions;