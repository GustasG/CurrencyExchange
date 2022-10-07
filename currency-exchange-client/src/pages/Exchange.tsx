import { useEffect } from "react";
import { toast } from "react-toastify";
import { Row, Col, } from "react-bootstrap";

import DropDown from "../components/DropDown";
import NumberInput from "../components/NumberInput";

import { useAppSelector, useAppDispatch } from "../hooks/typedActions";

import { setCurrencyFrom, setCurrencyTo, setAmount, setRate } from "../redux/slices/exchangeSlice";
import { useGetCurrenciesQuery, useGetExchangeRateMutation } from "../redux/api/exchangeApi";

export default function Exchange() {
  const { data: currencies, error: currencyFetchError, isLoading: isLoadingCurrency } = useGetCurrenciesQuery();
  const [updateExchangeRate] = useGetExchangeRateMutation();

  const dispatch = useAppDispatch();
  const currencyFrom = useAppSelector(state => state.exchange.currencyFrom);
  const currencyTo = useAppSelector(state => state.exchange.currencyTo);
  const amount = useAppSelector(state => state.exchange.amount);
  const rate = useAppSelector(state => state.exchange.rate);

  const shouldUpdateCurrencyFrom = currencyFrom === "";
  const shouldUpdateCurrencyTo = currencyTo === "";

  useEffect(() => {
    if (currencies) {
      if (shouldUpdateCurrencyFrom) {
        dispatch(setCurrencyFrom(currencies[0]));
      }
      if (shouldUpdateCurrencyTo) {
        dispatch(setCurrencyTo(currencies[0]));
      }
  }
  }, [currencies, shouldUpdateCurrencyFrom, shouldUpdateCurrencyTo, dispatch]);

  useEffect(() => {
    if (currencyFrom === "" || currencyTo === "") {
      return;
    }

    updateExchangeRate({
      from: currencyFrom,
      to: currencyTo
    })
    .unwrap()
    .then(data => dispatch(setRate(data)))
    .catch(() => {
      toast.error("Failed to fetch exchange rate");
      dispatch(setRate(0));
    });
  }, [currencyFrom, currencyTo, updateExchangeRate, dispatch]);

  if (isLoadingCurrency) {
    return <p>Loading...</p>
  }

  if (currencyFetchError || !currencies) {
    return <p>Failed to fetch available currencies</p>
  }

  const exchangedAmount = rate * amount;

  return (
    <>
      <h1>Convert currency</h1>
      <hr />
      <Row className="pt-4">
        <Col md={4} className="pb-4">
          <DropDown label="From" options={currencies} value={currencyFrom} onChange={(value) => dispatch(setCurrencyFrom(value))} />
          <DropDown label="To" options={currencies} value={currencyTo} onChange={(value) => dispatch(setCurrencyTo(value))} />
          <NumberInput label="Amount" initialValue={amount} onChange={(value) => dispatch(setAmount(value))} />
        </Col>
        <Col md={{span: 4, offset: 2}}>
          <p>{currencyFrom}/{currencyTo} Exchange Rate: <span>{rate.toFixed(2)}</span></p>
          <p>Converted Amount: <span>{exchangedAmount.toFixed(2)}</span></p>
        </Col>
      </Row>
    </>
  );
}